import React, { useEffect, useState } from 'react';
import {
  Button,
  Snackbar,
  Alert,
  Fab,
  Tooltip,
  Zoom,
} from '@mui/material';
import {

  InstallDesktop,
  Close,
} from '@mui/icons-material';

interface BeforeInstallPromptEvent extends Event {
  prompt: () => Promise<void>;
  userChoice: Promise<{ outcome: 'accepted' | 'dismissed' }>;
}

declare global {
  interface WindowEventMap {
    beforeinstallprompt: BeforeInstallPromptEvent;
  }
}

const InstallButton: React.FC = () => {
  const [installPrompt, setInstallPrompt] = useState<BeforeInstallPromptEvent | null>(null);
  const [isInstallable, setIsInstallable] = useState(false);
  const [showInstallSuccess, setShowInstallSuccess] = useState(false);
  const [isInstalled, setIsInstalled] = useState(false);
  const [dismissed, setDismissed] = useState(false);

  useEffect(() => {
    // Verificar se já está instalado (modo standalone)
    const isStandalone = window.matchMedia('(display-mode: standalone)').matches ||
                        (window.navigator as any).standalone ||
                        document.referrer.includes('android-app://');

    setIsInstalled(isStandalone);

    // Se já estiver instalado, não mostra o botão
    if (isStandalone) {
      return;
    }

    // Capturar o evento beforeinstallprompt
    const handleBeforeInstallPrompt = (e: BeforeInstallPromptEvent) => {
      e.preventDefault();
      setInstallPrompt(e);
      setIsInstallable(true);
    };

    window.addEventListener('beforeinstallprompt', handleBeforeInstallPrompt);

    // Verificar se o app já foi instalado
    window.addEventListener('appinstalled', () => {
      setIsInstallable(false);
      setInstallPrompt(null);
      setIsInstalled(true);
      setShowInstallSuccess(true);
    });

    return () => {
      window.removeEventListener('beforeinstallprompt', handleBeforeInstallPrompt);
    };
  }, []);

  const handleInstallClick = async () => {
    if (!installPrompt) return;

    // Mostrar o prompt de instalação
    await installPrompt.prompt();

    // Aguardar a escolha do usuário
    const choiceResult = await installPrompt.userChoice;

    if (choiceResult.outcome === 'accepted') {
      console.log('Usuário aceitou instalar');
      setInstallPrompt(null);
      setIsInstallable(false);
    } else {
      console.log('Usuário recusou instalar');
      setDismissed(true);
    }
  };

  const handleCloseSuccess = () => {
    setShowInstallSuccess(false);
  };

  // Se já estiver instalado ou não for instalável ou já foi dispensado, não mostra nada
  if (isInstalled || !isInstallable || dismissed) {
    return null;
  }

  return (
    <>
      {/* Botão flutuante de instalação */}
      <Zoom in={true}>
        <Tooltip title="Instalar aplicativo" placement="left">
          <Fab
            color="primary"
            aria-label="install"
            onClick={handleInstallClick}
            sx={{
              position: 'fixed',
              bottom: 16,
              right: 16,
              zIndex: 1000,
            }}
          >
            <InstallDesktop />
          </Fab>
        </Tooltip>
      </Zoom>

      {/* Snackbar de sucesso */}
      <Snackbar
        open={showInstallSuccess}
        autoHideDuration={6000}
        onClose={handleCloseSuccess}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
      >
        <Alert 
          onClose={handleCloseSuccess} 
          severity="success" 
          sx={{ width: '100%' }}
          action={
            <Button color="inherit" size="small" onClick={handleCloseSuccess}>
              <Close fontSize="small" />
            </Button>
          }
        >
          Aplicativo instalado com sucesso! 🎉
        </Alert>
      </Snackbar>
    </>
  );
};

export default InstallButton;